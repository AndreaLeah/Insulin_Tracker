<!-- Main Application Template. 
     Note that you have classes from bootstrap available to you.
     See https://getbootstrap.com/docs/4.5/getting-started/introduction/ for reference on bootstrap -->
<template>
  <div id="app" :class='[{"AddProfileBackground": isAddProfile}, {"HomeStyling": isHomePage}]'> <!-- If you start to get random styling you don't like, remove container from this div -->
    <Nav />
    <router-view />
  </div>
</template>

<script>
import Nav from './components/Nav.vue';

export default {
  components: { 
    Nav
  },
  computed: {
    isAddProfile() {
      return this.$route.name === 'AddProfile';
    },
    isHomePage() {
      return this.$route.name === 'home';
    }
  }
}
</script>

<!-- Application-Wide Styles go here. 
     Any valid CSS or SCSS is allowable here. See https://sass-lang.com/documentation/style-rules for info on SCSS -->
<style lang="scss">

@import './styles/colors.scss';

// Import the bootstrap styles. If you want to override Bootstrap colors, it needs to happen above this line
@import '../node_modules/bootstrap/scss/bootstrap';

// Your custom styles go below this point

a{
    text-decoration: none;
}

div {
  color: #746f6f;
}

h1, h2, h3 {
  color: rgb(255, 87, 87);
}

input {
  border-color: #F5EDED;
  border-radius: 5px;
}

</style>