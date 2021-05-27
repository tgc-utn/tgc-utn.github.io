# tgc-utn.github.io [![Build and deploy Jekyll site to GitHub Pages](https://github.com/tgc-utn/tgc-utn.github.io/actions/workflows/github-pages.yml/badge.svg)](https://github.com/tgc-utn/tgc-utn.github.io/actions/workflows/github-pages.yml)

Repositorio de la página web de [TGC](http://tgc-utn.github.io/).

### Puesta en marcha (Debian / Ubuntu)

Esta es una web contruida con [Jekyll](https://jekyllrb.com/) y alojada en [GitHub Pages](https://pages.github.com/). Para poder levantar el sitio primero verifique los [requerimientos](https://jekyllrb.com/docs/installation/#requirements) de Jekyll. 
Usamos el theme [Just the docs](https://pmarsceill.github.io/just-the-docs/)

Vamos a necesitar tener instalado primero [Ruby](https://www.ruby-lang.org/).

```bash
sudo apt-get install ruby-full build-essential zlib1g-dev
echo '# Install Ruby Gems to ~/gems' >> ~/.bashrc
echo 'export GEM_HOME="$HOME/gems"' >> ~/.bashrc
echo 'export PATH="$HOME/gems/bin:$PATH"' >> ~/.bashrc
source ~/.bashrc
gem install jekyll bundler
```
Siguiendo estos pasos vamos a poder copiar el repositorio a la computadora y construir / levantar el sitio:

```bash
git clone https://github.com/tgc-utn/tgc-utn.github.io.git
cd tgc-utn.github.io
bundle install

bundle exec jekyll serve --livereload --config _config.yml,_config_dev.yml
# => Listo solo hay que entrar a http://localhost:4000
```

### Edición
Completar esta sección :laughing:

[Jekyll](https://jekyllrb.com/) soporta por defecto las siguientes tecnologías:
* [kramdown](https://kramdown.gettalong.org/)

Algunas cosas que pueden ayudar
* [Mastering Markdown](https://guides.github.com/features/mastering-markdown/)
* [Markdown editor](https://dillinger.io/)
