---
layout: default
title: Calendario
---

# Calendario de la cursada

| Clase | Fecha | Actividad | Trabajo Práctico |
| :---: | :---: | :-------: | :--------------: |
{% for row in site.data.cronograma -%}
|{{forloop.index}}|{{ row["fecha"] | localize: "%d de %B" }} | {{ row["actividad"] }} | {{ row["tp"] }} |
{% endfor -%}
{: .cronograma }

- [Calendario Académico](https://frba.utn.edu.ar/static/CalendarioAcademico2023.pdf)
