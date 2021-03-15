---
layout: default
title: Cronograma
navHome: true
permalink: /clases/cronograma/
---

* [Calendario Académico](http://siga.frba.utn.edu.ar/up/docs/CalendarioAcademico2021.pdf)

## Calendario de la cursada

| Clase | Fecha               | Actividad    | Trabajo Práctico  |
|:-----:|:-------------------:|:------------:|:-----------------:|
{% for row in site.data.cronograma -%}
|{{forloop.index}}|{{ row["fecha"] | localize: "%d de %B" }} | {{ row["actividad"] }} | {{ row["tp"] }} |
{% endfor -%}
{: .cronograma }

