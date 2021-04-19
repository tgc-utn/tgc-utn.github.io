#version 300 es

precision highp float;
in vec2 fragCoord;
out vec4 glFragColor;
uniform vec3 iResolution;
uniform float iGlobalTime;
vec2 iMouse;


float sdSegment( in vec2 p, in vec2 a, in vec2 b )
{
    vec2 pa = p-a, ba = b-a;
    float h = clamp( dot(pa,ba)/dot(ba,ba), 0.0, 1.0 );
    return length( pa - ba*h );
}

vec2 random2( vec2 p ) {
    return fract(sin(vec2(dot(p,vec2(127.1,311.7)),dot(p,vec2(269.5,183.3))))*43758.5453);
}

float invLerp(float from, float to, float value){
  return (value - from) / (to - from);
}



float saturate(float value)
{
    return clamp(value, 0.0, 1.0);
}


vec3 saturate(vec3 value)
{
    return clamp(value, vec3(0.0), vec3(1.0));
}

float point(vec2 current, vec2 target)
{    
    return smoothstep(0.0, 1.4, 0.01 / distance(current, target));
}

float pointInLine(vec2 current, vec2 target)
{    
    return smoothstep(0.0, 1.6, 0.01 / distance(current, target));
}

float line(vec2 point, vec2 a, vec2 b)
{
    float lineDistance = abs(sdSegment(point, a, b));    
    float lineLength = smoothstep(2.0, 0.2, distance(a, b));
    return saturate(smoothstep(0.01, 0.0, lineDistance) * lineLength);
}

float lineNoDistanceFade(vec2 point, vec2 a, vec2 b)
{    
    float lineDistance = abs(sdSegment(point, a, b));    
    return saturate(smoothstep(0.005, 0.0, lineDistance));
}

vec4 positionsForTime(float time, float scale)
{
    float offseted = time + 0.05;
    
    vec4 positions;
    positions.xy = random2(vec2(time, time));       
    positions.zw = random2(vec2(offseted, offseted));    
    
    positions *= scale;
    
    positions.zw *= 1.2;
    return positions;
}

const int indexPerDifference[] = int[8]
(
    0,
    1,
    2,
    3,
    4,
    5,
    6,
    7
);

int indexFromDifference(vec2 difference)
{
    float preIndex = difference.x * 3.0 + difference.y;
    
    preIndex -= max(sign(preIndex), 0.0);
    int index = int(preIndex) + 4;        
    
    return indexPerDifference[index];
}




int inLine(vec2 start, vec2 end, vec2 position, int head, out vec4 positions)
{
    int x = int(start.x);
    int y = int(start.y);
    
    int dx = abs(x - int(end.x));
    int dy = abs(y - int(end.y));
    
    int sx = start.x < end.x ? 1 : -1;
    int sy = start.y < end.y ? 1 : -1;
    
    int err = (dx > dy ? dx : -dy) / 2;
    
    
    int index = head+1;
    while(index > 0)
    {   
        positions.xy = vec2(x, y);
        int e2 = err;
        
        if (e2 > -dx)
        {
            err -= dy;
            x += sx;
        }
        
        if (e2 < dy)
        {
            err += dx;
            y += sy;
        }
        index--;
    }
    
    positions.zw = vec2(x, y);
    vec2 difference = abs(positions.zw - position);
    vec2 difference2 = abs(positions.xy - position);
    vec2 add = difference2 + difference;
    
    if(difference.x == 0.0 && difference.y == 0.0)
        return 1;
    else if(difference2.x == 0.0 && difference2.y == 0.0)
        return 3;
    else if(add == vec2(1.0, 1.0))
        return 2;
    else
        return 0;
}

vec3 flashLines(in vec2 cell, in vec2 cellInner, in vec2 positions[8], float scale, vec2 cellInnerCoordinates)
{    
    float timeScaled = iGlobalTime * 1.1;
    float floorTime = floor(timeScaled);    
    
    
    if(random2(vec2(floorTime, floorTime)).x > 0.2)
        return vec3(0.0);
    
    float time = fract(timeScaled);
    
    vec4 headCells;
    
    vec4 timedPositions = positionsForTime(floorTime, scale);
    timedPositions.yw += iGlobalTime * 0.15;
    
    vec4 flooredPositions = floor(timedPositions);    
    float distanceBetweenPositions = distance(flooredPositions.xy, flooredPositions.zw);    
    vec2 difference = abs(flooredPositions.zw - flooredPositions.xy);
    float amount = max(difference.x, difference.y);
    float ti = amount * time;
    
    int result = inLine(timedPositions.xy, timedPositions.zw, cell, int(ti), headCells);
    
    if(result == 0)
        return vec3(0.0);
    else
    {
        vec2 positionA;
        vec2 positionB;
        vec3 pointAndLine;
        
        if(result == 1)
        {
            
            positionA = positions[indexFromDifference(headCells.xy - cell)];                        
            positionB = cellInner;
            pointAndLine.x = point(cellInnerCoordinates, cellInner); 
        }
        else if (result == 2)
        {   
            headCells -= cell.xyxy;
            
            positionA = positions[indexFromDifference(headCells.xy)];
            positionB = positions[indexFromDifference(headCells.zw)];    
        }
        else if(result == 3)
        {           
            positionB = positions[indexFromDifference(headCells.zw - cell)]; 
            positionA = cellInner;              
            pointAndLine.x = point(cellInnerCoordinates, cellInner);                                    
        }
    
        pointAndLine.y = lineNoDistanceFade(cellInnerCoordinates, positionA, positionB);    
        vec2 mid = mix(positionA, positionB, fract(ti));
        pointAndLine.z = pointInLine(cellInnerCoordinates, mid);    
        return pointAndLine;
    }         
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    // Normalized pixel coordinates (from 0 to 1)
    vec2 uv = fragCoord/iResolution.xx;
    
    float scale = 15.0;

    uv *= scale;
    uv.y += iGlobalTime * 0.15;
    
    vec2 cellNumber = floor(uv);
    vec2 cellInnerCoordinates = fract(uv);
    
    vec2 actualCell, randomPoint;
    vec2 addedCell;    
    
    float points = 0.0;
    float lines = 0.0;
    
    vec2 centeredPoint = random2(cellNumber);    
    centeredPoint = 0.5 + 0.5*sin(iGlobalTime + 6.2831*centeredPoint );
    
    vec2 pointPositions[8];
    vec2 actualPositions[8];
    
    int index = 0;
    
    for(int x = -1; x <= 1; x++)
        for(int y = -1; y <= 1; y++)
        {                
            if(x == 0 && y == 0)
                continue;
            addedCell = vec2(float(x), float(y));            
            actualCell = cellNumber + addedCell;
            randomPoint = random2(actualCell);            
            randomPoint = 0.5 + 0.5 * sin(iGlobalTime + 6.2831 * randomPoint);
            pointPositions[index] = addedCell + randomPoint;
            actualPositions[index] = actualCell;
            index++;
        }
    
    for(index = 0; index < 8; index++)
    {
        vec2 randomPoint = pointPositions[index];                    
        lines += line(cellInnerCoordinates, centeredPoint, randomPoint);
        points += point(randomPoint, cellInnerCoordinates);      
    }
    
    points += point(centeredPoint, cellInnerCoordinates);        
      
    vec2 top = pointPositions[1];
    vec2 left = pointPositions[3];
    vec2 right = pointPositions[4];  
    vec2 bottom = pointPositions[6];
    
    // Left top    
    lines += line(cellInnerCoordinates, left, top);    
   
    // Left bottom    
    lines += line(cellInnerCoordinates, left, bottom);
    
    // Right top      
    lines += line(cellInnerCoordinates, right, top);
    
    // Right bottom    
    lines += line(cellInnerCoordinates, right, bottom);
    
    
    //vec3 color = vec3(0.015 / minimumDistance);
    vec3 color;
    
    vec3 flash = flashLines(cellNumber, centeredPoint, pointPositions, scale, cellInnerCoordinates);    
       
    vec3 co = vec3(0.83, 0.15, 0.455);
   
    color.rgb += points;
    color.rgb += vec3(0.0, 0.45, 0.9) * lines;
    
    
    color.rgb = mix(vec3(0.0), vec3(0.0, 0.45, 0.9), lines);
    
    color.rgb = mix(color.rgb, co, flash.x + flash.y * 0.35 + flash.z * .5); 
    color.rgb = mix(color.rgb, vec3(1.0), saturate(flash.x * 1.05 - .4) +  saturate(flash.y * 0.35 * 1.5 - 0.625) + saturate(flash.z * 1.05 - 0.625) + points);
    
    // Draw grid
    //color.r += step(cellInnerCoordinates.x, 0.02) + step(cellInnerCoordinates.y, 0.02);
  
    // Output to screen
    fragColor = vec4(color, 1.0);
}


void main () {
	iMouse = vec2(0.0, 0.0);

	mainImage(glFragColor, fragCoord);
}
