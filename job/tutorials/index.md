---
layout: default
title: Tutoriales
parent: Trabajo
---

# Tutoriales de MonoGame

<ul>
  {% for tutorial in site.monogame-tutorials %}
    <li>
      <a href="{{ tutorial.url }}">{{ tutorial.title }}</a>
    </li>
  {% endfor %}
</ul>
