<template>
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
      <router-link 
        class="nav-item"
        v-bind:to="{name: 'Profile'}">
        &nbsp;|&nbsp;View Profile
        </router-link>
    </div>
</template>

<script>
export default {

}
</script>

<style>

#nav {
  width: 100%;
  height: 100px;
}

</style>