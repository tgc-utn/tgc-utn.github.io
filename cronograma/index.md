---
layout: default
title: Cronograma
navHome: true
permalink: /clases/cronograma/
---

<style>

table {
  border-collapse: collapse;
  border-spacing: 0;
  font-size: 1em;
  border: none;
  margin: 0;
  padding: 0.5em 1em;
}

th {
 font-weight: bold;
  background-color: #F0F0F0;
  border:0px solid #000000;
}

td{
    border:1px solid #000000;
}

</style>

* [Calendario Académico](http://siga.frba.utn.edu.ar/up/docs/CalendarioAcademico2021.pdf)

## Calendario de la cursada


| Clase | Fecha               | Actividad    | Trabajo Práctico  |
|:-----:|:-------------------:|:------------:|:-----------------:|
{% for row in site.data.cronograma -%}
|{{forloop.index}}|{{ row["fecha"] | localize: "%d de %B" }} | {{ row["actividad"] }} | {{ row["tp"] }} |
{% endfor -%}


