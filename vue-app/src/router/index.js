import Vue from "vue";
import VueRouter from "vue-router";
import HomeView from "@/views/Main/HomeView";
// import AboutView from "@/views/Main/AboutView";
import ProductList from "@/views/AdminDashboard/ProductList";
import DeviceList from "@/views/AdminDashboard/DeviceList";

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
    component: () => import("../views/Main/AboutView.vue"),
  },
  {
    path: "/login",
    name: "login",
    //component: () => import("@/views/Main/AboutView"),
    component: () => import("../views/Main/LoginView.vue"),
  },
  {
    path: "/admin-dashboard",
    component: () => import("@/views/AdminDashboard"),
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

export default router;
