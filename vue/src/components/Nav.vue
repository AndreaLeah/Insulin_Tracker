<template>
  <div id="nav">
    <div class="nav-container">
      <div class="nav-content">

        <router-link v-bind:to="{ name: 'home' }" id="logo" class="items">
          <img class="logo" src="../assets/InsuTrakLogo.png" alt="InsuTrak Logo">
        </router-link>

        <router-link
          class="nav-item items"
          v-bind:to="{ name: 'register' }"
          v-if="!$store.state.token">
          Register
        </router-link>

        <router-link 
          class="nav-item items"
          v-bind:to="{name: 'Profile'}"
          v-if="$store.state.token">
          View Profile
        </router-link>

        <router-link
          class="nav-item items"
          v-bind:to="{ name: 'activity' }"
          v-if="$store.state.token">
          My Activity
        </router-link>

        <router-link
          class="nav-item items"
          v-bind:to="{ name: 'login' }"
          v-if="!$store.state.token">
          Login
        </router-link>

        <router-link 
          class="nav-item items" 
          v-bind:to="{ name: 'about' }">
          About This Team 
        </router-link>

        <router-link
          class="nav-item items"
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

.logo{
  height: 100%;
  width: 112px;
  border-radius: 0;
}

.logo:hover{
  border: thin solid rgb(255, 146, 146);
  border-radius: 4px;
}

.items {
  text-decoration: none;
}

#nav {
  height: 40px;
  background-color: rgb(153, 6, 6);
  display: flex;
  justify-items: center;
  justify-content: flex-end;
  width: 100%;
}

.nav-container {
  display: flex;
  justify-content: space-around;
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
  display: flex;
  font-weight: bold;
  color: rgb(255, 123, 123);
  font-size: 28px;
}

#logo:hover {
  text-decoration: none;
  color: rgb(255, 146, 146);
}

.nav-item {
  color: white;
  font-weight: bold;
  white-space: nowrap;
}

.nav-item:hover {
  text-decoration: none;
  color: rgb(255, 215, 215);
}

/* Tablet */

@media only screen and (max-width: 850px) {

  

}

/* Phones */

@media only screen and (max-width: 696px) {

  .nav-container {
    width: 60%;
  }

}



</style>