---
layout: page
title: Trabajos
navHome: true
permalink: /trabajos/
---

* [Trabajo práctico actual: descarga del TP, crear grupo, etc.](/trabajos/actual/)
* [Trabajos de Investigación: papers y temas investigados por alumnos](/trabajos/investigacion/)

### Anteriores

<iframe width="425" height="355" src="https://youtu.be/fFMRj7Eu8sU" frameborder="0" allowfullscreen></iframe>

<ul class="posts">
   {% for post in site.posts %}
     <li><a class="post-link" href="{{ post.url }}">{{ post.title }}</a></li>
   {% endfor %}
</ul>
