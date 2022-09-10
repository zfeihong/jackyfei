<template>
  <v-skeleton-loader
    v-if="loading"
    width="300"
    max-width="450"
    height="100%"
    type="card"
  ></v-skeleton-loader>
  <v-card v-else width="300" max-width="450" height="100%">
    <v-toolbar color="light-blue" dark>
      <v-toolbar-title>产品列表</v-toolbar-title>
      <v-spacer></v-spacer>
    </v-toolbar>
    <v-list-item-group color="primary">
      <v-list-item v-for="product in lists" :key="product.id">
        <v-list-item-content @click="addToDevice(product.devices, product.id)">
          <v-list-item-title v-text="product.id"></v-list-item-title>
          <v-list-item-subtitle v-text="product.name"></v-list-item-subtitle>
        </v-list-item-content>
        <v-list-item-content>
          <v-list-item-title v-text="product.code"></v-list-item-title>
          <v-list-item-subtitle
            v-text="product.category"
          ></v-list-item-subtitle>
        </v-list-item-content>
        <v-list-item-action>
          <div class="mr-2">
            {{ product.device && product.device.length }}
          </div>
          <v-icon @click="removeProduct(product.id)">
            mdi-delete-outline
          </v-icon>
        </v-list-item-action>
      </v-list-item>
    </v-list-item-group>
  </v-card>
</template>

<script>
import { mapActions, mapGetters } from "vuex";

export default {
  name: "ProductCard",

  computed: {
    ...mapGetters("productModule", {
      lists: "lists",
      loading: "loading",
    }),
  },

  methods: {
    ...mapActions("productModule", [
      "removeProductAction",
      "getDevicesOfSelectedProductIdAction",
    ]),
    removeProduct(listId) {
      const confirmed = confirm("确定删除产品？");

      if (!confirmed) return;
      this.removeProductAction(listId);
    },
    addToDevice(devices, listId) {
      this.getDevicesOfSelectedProductIdAction(devices);
      this.$emit("handleDevices", true, listId);
    },
  },
};
</script>
