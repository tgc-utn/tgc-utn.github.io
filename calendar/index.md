---
layout: default
title: Calendario
---

# Calendario de la cursada

| Clase | Fecha | Actividad | Trabajo Práctico | Modalidad |
| :---: | :---: | :-------: | :--------------: | :-------: |
{% for row in site.data.cronograma -%}
|{{forloop.index}}|{{ row["fecha"] | localize: "%d de %B" }} | {{ row["actividad"] }} | {{ row["tp"] }} | {{ row["modalidad"] }} |
{% endfor -%}
{: .cronograma }

- [Calendario Académico](https://frba.utn.edu.ar/static/CALENDARIO2024.pdf)
