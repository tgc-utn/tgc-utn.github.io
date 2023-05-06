# tgc-utn.github.io [![Build and deploy Jekyll site to GitHub Pages](https://github.com/tgc-utn/tgc-utn.github.io/actions/workflows/pages.yml/badge.svg)](https://github.com/tgc-utn/tgc-utn.github.io/actions/workflows/pages.yml)

Repositorio de la página web de [TGC](https://tgc-utn.github.io/).

### Puesta en marcha (Debian / Ubuntu)

Esta es una web contruida con [Jekyll](https://jekyllrb.com/) y alojada en [GitHub Pages](https://pages.github.com/). Para poder levantar el sitio primero verifique los [requerimientos](hhttps://jekyllrb.com/docs/installation/#requirements) de Jekyll. 
Usamos el theme [Just the docs](https://github.com/just-the-docs/just-the-docs) y para los shaders [Shaderback.js](https://github.com/llewelld/shaderback.js).

### Levantar el sitio

```bash
git clone https://github.com/tgc-utn/tgc-utn.github.io.git
cd tgc-utn.github.io
bundle install

bundle exec jekyll serve
# => Listo solo hay que entrar a http://localhost:4000
```
