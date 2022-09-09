<template>
  <v-row justify="center">
    <v-dialog v-model="dialog" persistent max-width="600px">
      <template v-slot:activator="{ on, attrs }">
        <v-btn
          style="margin-top: 1rem"
          rounded
          color="pink"
          dark
          v-bind="attrs"
          v-on="on"
        >
          <v-icon left> mdi-plus </v-icon>
          添加新的设备
        </v-btn>
      </template>
      <v-card>
        <form @submit.prevent="onSubmit">
          <v-card-title>
            <span class="headline">添加新的设备</span>
          </v-card-title>
          <v-card-text>
            <v-container>
              <v-row>
                <v-col cols="12">
                  <v-text-field
                    label="Name"
                    v-model="bodyRequest.name"
                    required
                  ></v-text-field>
                </v-col>
                <v-col cols="12">
                  <v-textarea
                    label="Code"
                    v-model="bodyRequest.code"
                    required
                  ></v-textarea>
                </v-col>
                <v-col cols="12">
                  <v-text-field
                    label="秘钥"
                    v-model="bodyRequest.secret"
                    required
                  ></v-text-field>
                </v-col>
                <v-col cols="12" sm="4">
                  <v-text-field
                    label="productId"
                    v-model="bodyRequest.productId"
                    required
                  ></v-text-field>
                </v-col>
                <!-- <v-col cols="12" sm="4">
                  <v-autocomplete
                    :items="durations"
                    v-model="bodyRequest.groupId"
                    label="Duration in hours"
                  ></v-autocomplete>
                </v-col> -->
              </v-row>
            </v-container>
            <small>*必填项</small>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" text @click="dialog = false">
              Close
            </v-btn>
            <v-btn
              color="blue darken-1"
              text
              @click="dialog = false"
              type="submit"
            >
              Save
            </v-btn>
          </v-card-actions>
        </form>
      </v-card>
    </v-dialog>
  </v-row>
</template>

<script>
import { mapActions } from "vuex";

export default {
  name: "AddDeviceForm2",

  props: {
    productId: {
      type: Number,
    },
  },

  data: () => ({
    id: 0,
    bodyRequest: {
      productId: 0,
      code: 0,
      name: "",
      secret: "",
      groupId: 10,
    },

    dialog: false,
    currencies: ["USD", "NOK"],
    currencyValues: [0, 1],
    durations: [1, 2, 3, 4, 5, 6, 7, 8],
    durationValue: 1,
  }),

  methods: {
    ...mapActions("productModule", ["addDeviceAction"]),

    onSubmit() {
      this.bodyRequest.productId = this.productId;
      this.addDeviceAction(this.bodyRequest);
      this.bodyRequest = {};
    },
  },
};
</script>
