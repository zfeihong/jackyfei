<template>
  <v-container>
    <v-card width="500" max-width="600">
      <v-toolbar color="pink" dark>
        <v-toolbar-title>设备信息</v-toolbar-title>
        <v-spacer></v-spacer>
      </v-toolbar>
      <v-list two-line>
        <v-list-item-group active-class="pink--text">
          <div v-if="devices && devices.length > 0">
            <template v-for="device in devices">
              <v-list-item :key="device.id">
                <template>
                  <v-list-item-content>
                    <v-list-item-title v-text="device.name"></v-list-item-title>
                    <v-list-item-subtitle
                      class="text--primary"
                      v-text="device.code"
                    ></v-list-item-subtitle>
                    <div
                      style="
                        margin-top: 0.5rem;
                        display: flex;
                        flex-direction: row;
                      "
                    >
                      <v-list-item-subtitle
                        class="text--secondary"
                        v-text="`Duration: ${device.id}hrs`"
                      ></v-list-item-subtitle>
                      <v-list-item-subtitle
                        class="text--secondary"
                        v-text="
                          `${device.Secret ? 'Instant Confirmation' : ''}`
                        "
                      ></v-list-item-subtitle>
                    </div>
                  </v-list-item-content>
                  <v-list-item-action>
                    <UpdateDeviceForm :bodyRequest="device" />
                    <v-icon @click="removeDevice(device.id)">
                      mdi-delete-outline
                    </v-icon>
                  </v-list-item-action>
                </template>
              </v-list-item>
            </template>
            <v-divider />
          </div>
          <div v-else>
            <v-list-item>
              <v-list-item-content>
                <v-list-item-title
                  v-text="'No package added yet 😢'"
                ></v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </div>
        </v-list-item-group>
      </v-list>
    </v-card>
  </v-container>
</template>

<script>
import { mapActions, mapGetters } from "vuex";
import UpdateDeviceForm from "@/components/UpdateDeviceForm";

export default {
  name: "DeviceCard",

  components: { UpdateDeviceForm },

  computed: {
    ...mapGetters("productModule", {
      devices: "devicesOfSelectedProductId",
    }),
  },

  methods: {
    ...mapActions("productModule", ["removeDeviceAction"]),

    removeDevice(deviceId) {
      const confirmed = confirm("确定删除?");
      if (!confirmed) return;
      this.removeDeviceAction(deviceId);
    },
  },
};
</script>
