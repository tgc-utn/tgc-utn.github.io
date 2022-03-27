---
layout: page
title: Trabajos
navHome: true
permalink: /trabajos/
---

* [Trabajo práctico actual: descarga del TP, crear grupo, etc.](/trabajos/actual/)
* [Trabajos de Investigación: papers y temas investigados por alumnos](/trabajos/investigacion/)

### Anteriores

<iframe width="560" height="315" src="https://www.youtube.com/embed/fFMRj7Eu8sU?controls=0" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

<ul class="posts">
   {% for post in site.posts %}
     <li><a class="post-link" href="{{ post.url }}">{{ post.title }}</a></li>
   {% endfor %}
</ul>
