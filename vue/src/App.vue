<!-- Main Application Template. 
     Note that you have classes from bootstrap available to you.
     See https://getbootstrap.com/docs/4.5/getting-started/introduction/ for reference on bootstrap -->
<template>
  <div id="app" :class='[{"AddProfileBackground": isAddProfile}, {"container": true}]'> <!-- If you start to get random styling you don't like, remove container from this div -->
    <div id="nav">
      <router-link class="nav-item" v-bind:to="{ name: 'home' }">
        <i class="fas fa-home"></i> <!-- This is a font awesome icon -->
        Home 
      </router-link>
      <router-link
        class="nav-item"
        v-bind:to="{ name: 'register' }"
        v-if="!$store.state.token">
        &nbsp;|&nbsp;Register
        </router-link>
      <router-link
        class="nav-item"
        v-bind:to="{ name: 'login' }"
        v-if="!$store.state.token">
        &nbsp;|&nbsp;Login
      </router-link>
      <router-link
        class="nav-item"
        v-bind:to="{ name: 'logout' }"
        v-if="$store.state.token">
        &nbsp;|&nbsp;Logout
      </router-link>
      <router-link 
        class="nav-item" 
        v-bind:to="{ name: 'about' }">
        &nbsp;|&nbsp;About this team 
      </router-link>
    </div>
    
    <router-view />
  </div>
</template>

<script>
export default {
  computed: {
    isAddProfile() {
      return this.$route.name === 'AddProfile';
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

body{
  background: #F5EDED;
  color: rgb(233, 64, 64);
  display: flex;
  justify-content: center;
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