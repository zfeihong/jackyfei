<template>
  <v-app-bar app color="primary" dark>
    <div class="d-flex align-center">
      <router-link to="/" class="menu">
        <v-img
          alt="Vuetify Logo"
          class="shrink mr-2"
          contain
          src="https://cdn.vuetifyjs.com/images/logos/vuetify-logo-dark.png"
          transition="scale-transition"
          width="40"
      /></router-link>
    </div>

    <v-spacer></v-spacer>

    <div>
      <v-btn color="primary" outlined :to="{ path: '/' }">
        <span class="menu">主页</span>
      </v-btn>

      <!-- 这里使用的是Vue的Router -->
      <router-link to="/about">
        <v-btn color="primary" outlined>
          <span class="menu">关于</span>
        </v-btn>
      </router-link>

      <v-btn color="primary" outlined :to="{ path: '/admin-dashboard' }">
        <span class="menu">工作台</span>
      </v-btn>
      <v-btn color="primary" outlined :to="{ path: '/login' }">
        <span class="menu">登录</span>
      </v-btn>
    </div>
  </v-app-bar>
</template>

<script>
export default {
  name: "NavBar2",
};
</script>

<style scoped>
.menu {
  color: white;
  text-decoration: none;
}
</style>
