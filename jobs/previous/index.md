---
layout: default
title: Anteriores
parent: Trabajos
---

# Trabajos prácticos anteriores

<ul class="posts">
   {% for post in site.posts %}
     <li><a class="post-link" href="{{ post.url }}">{{ post.title }}</a></li>
   {% endfor %}
</ul>
