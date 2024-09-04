---
layout: default
title: Tutoriales
parent: Material
---

# Tutoriales de matemática y física

Algunos tutoriales/talleres de matemática y física con un enfoque para videojuegos.

<ul>
  {% for tutorial in site.material-tutorials %}
    <li>
      <a href="{{ tutorial.url }}">{{ tutorial.title }}</a>
    </li>
  {% endfor %}
</ul>


# Tutoriales de MonoGame

Estos tutoriales están diseñados para ofrecerte un enfoque práctico y directo sobre cómo usar MonoGame para desarrollar tus proyectos.

<ul>
  {% for tutorial in site.monogame-tutorials %}
    <li>
      <a href="{{ tutorial.url }}">{{ tutorial.title }}</a>
    </li>
  {% endfor %}
</ul>
