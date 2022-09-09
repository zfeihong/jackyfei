<template>
  <div>
    <div class="text-h2 my-4">欢迎来到 Admin Dashboard</div>
    <div class="default-content">
      <div style="margin-right: 4rem; margin-bottom: 4rem">
        <ProductCard @handleShowPackages="handleShowPackages" />
        <AddProductForm />
      </div>
      <div v-if="showDevices">
        <DeviceCard />
        <AddDeviceForm :productId="productId" />
      </div>
    </div>
  </div>
</template>

<script>
import { mapActions } from "vuex";
import ProductCard from "@/components/ProductCard";
import AddProductForm from "@/components/AddProductForm";
import DeviceCard from "@/components/DeviceCard";
import AddDeviceForm from "@/components/AddDeviceForm";

export default {
  name: "DefaultContent",
  components: {
    ProductCard,
    AddProductForm,
    DeviceCard,
    AddDeviceForm,
  },
  methods: {
    ...mapActions("productModule", ["getProductAction"]),
    handleShowPackages(show, listId) {
      this.showDevices = show;
      this.productId = listId;
    },
  },

  data: () => ({
    showDevices: false,
    productId: 0,
  }),

  mounted() {
    this.getProductAction();
    this.showDevices = false;
  },
};
</script>
<style scoped>
.default-content {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  justify-content: flex-start;
}
</style>
