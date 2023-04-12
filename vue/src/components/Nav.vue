<template>
  <div id="nav">
    <div class="nav-container">



      <div class="nav-content">

       
          <router-link v-bind:to="{ name: 'home' }" id="logo">
            Insu-Trak
          </router-link>
       

        <router-link class="nav-item" v-bind:to="{ name: 'home' }">
          <!-- <i class="fas fa-home"></i> This is a font awesome icon -->
          Home 
        </router-link>

        <router-link
          class="nav-item"
          v-bind:to="{ name: 'register' }"
          v-if="!$store.state.token">
          Register
          </router-link>

        <router-link 
          class="nav-item"
          v-bind:to="{name: 'Profile'}"
          v-if="$store.state.token">
          View Profile
          </router-link>

        <router-link
          class="nav-item"
          v-bind:to="{ name: 'login' }"
          v-if="!$store.state.token">
          Login
        </router-link>

        <router-link 
          class="nav-item" 
          v-bind:to="{ name: 'about' }">
          About This Team 
        </router-link>

        <router-link
          class="nav-item"
          v-bind:to="{ name: 'logout' }"
          v-if="$store.state.token">
          Logout
        </router-link>

      </div>
    </div>
  </div>
</template>

<script>
export default {

}
</script>

<style>

#nav {
  height: 40px;
  background-color: rgb(153, 6, 6);
  display: flex;
  justify-items: center;
  width: 100%;
}

.nav-container {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 20px;
  width: 50%;
  margin: 0 auto;
}


.nav-content {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 20px;
  position: relative;
}

#logo {
  font-weight: bold;
  color: rgb(255, 123, 123);
  font-size: 28px;
  position: absolute;
  left: -200px;
}

#logo:hover {
  text-decoration: none;
  color: rgb(255, 146, 146);
}

.nav-item {
  color: white;
  font-weight: bold;
}




</style>