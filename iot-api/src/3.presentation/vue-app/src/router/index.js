import Vue from "vue";
import VueRouter from "vue-router";
import HomeView from "@/views/Main/HomeView";
// import AboutView from "@/views/Main/AboutView";
import ProductList from "@/views/AdminDashboard/ProductList";
import DeviceList from "@/views/AdminDashboard/DeviceList";
import { authGuard } from "@/auth/auth.guard";
import { isTokenFromLocalStorageValid } from "@/auth/auth.service";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "home",
    component: HomeView,
    // component: () => import("@/views/Main/HomeView"),
  },
  {
    path: "/about",
    name: "about",
    //component: () => import("@/views/Main/AboutView"),
    // component: () => import("../views/Main/AboutView.vue"),
    component: () => import("@/views/Main/AboutView"),
    meta: { requiresAuth: false },
  },
  {
    path: "/login",
    name: "login",
    //component: () => import("@/views/Main/AboutView"),
    // component: () => import("../views/Main/LoginView.vue"),
    component: () => import("@/auth/views/LoginView"),
    meta: { requiresAuth: false },
    beforeEnter: (to, from, next) => {
      const valid = isTokenFromLocalStorageValid();
      console.log("VALID::", valid);
      if (valid) {
        next("/continue-as");
      } else {
        next();
      }
    },
  },
  {
    path: "/continue-as",
    component: () => import("@/auth/views/ContinueAs"),
    meta: {
      requiresAuth: false,
    },
  },
  {
    path: "/logout",
    beforeEnter() {
      localStorage.clear();
      window.location.href = "/";
    },
  },
  {
    path: "*",
    redirect: "/",
  },
  {
    path: "/admin-dashboard",
    component: () => import("@/views/AdminDashboard"),
    meta: { requiresAuth: true },
    children: [
      {
        path: "",
        component: () => import("@/views/AdminDashboard/DefaultContent"),
      },
      {
        path: "weather-forecast",
        component: () => import("@/views/AdminDashboard/WeatherForecast"),
      },
      /* eager loading through static import statement */
      {
        path: "product-list",
        component: ProductList,
      },
      {
        path: "device-list",
        component: DeviceList,
      },
    ],
  },
];

const router = new VueRouter({
  routes,
});

router.beforeEach((to, from, next) => {
  console.log("打印router.beforeEach");
  authGuard(to, from, next);
});

export default router;
