<template>
  <div class="container" v-if="email">
    <div class="text-h4 my-5">是否继续{{ email }}? 🔒</div>
    <v-btn @click="onSubmit" color="primary" class="mr-4">Yes</v-btn>
    <v-btn @click="handleLogOut()" outlined color="primary">我要注销</v-btn>
  </div>
  <div v-else class="text-h4 m-10">
    想不到在这里见到你。
    <v-btn @click="handleLogOut()" outlined color="primary"
      >注销，并再次登录</v-btn
    >
  </div>
</template>

<script>
import { getUserEmailFromToken, logOut } from "@/auth/auth.service";
export default {
  name: "ContinueAs",

  data: () => ({
    email: getUserEmailFromToken(),
  }),

  methods: {
    handleLogOut() {
      logOut();
    },
    onSubmit() {
      this.$router.push({ path: "/admin-dashboard" });
    },
  },
};
</script>
