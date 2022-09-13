<template>
  <v-row justify="center">
    <v-dialog v-model="dialog" persistent max-width="600px">
      <template v-slot:activator="{ on, attrs }">
        <v-icon class="mr-3" v-bind="attrs" v-on="on">
          mdi-clipboard-edit-outline
        </v-icon>
      </template>
      <v-card>
        <form @submit.prevent="onSubmit">
          <v-card-title>
            <span class="headline">更新设备</span>
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
                  <v-text-field
                    label="Code"
                    v-model="bodyRequest.code"
                    required
                  ></v-text-field>
                </v-col>
                <v-col cols="12">
                  <v-text-field
                    label="Secret"
                    v-model="bodyRequest.secret"
                    required
                  ></v-text-field>
                </v-col>
                <v-col cols="12" sm="4">
                  <v-text-field
                    label="GroupId"
                    v-model="bodyRequest.groupId"
                    required
                  ></v-text-field>
                </v-col>
              </v-row>
            </v-container>
            <small>*必填</small>
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
              Update
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
  name: "UpdateDeviceForm",

  props: {
    bodyRequest: {
      type: Object,
      required: true,
      // eslint-disable-next-line vue/require-valid-default-prop
      default: {
        id: 0,
        name: "missing name",
        code: "missing code",
        secret: "missing map Secret",
        groupId: 0,
      },
    },
  },

  methods: {
    ...mapActions("productModule", ["updateDeviceAction"]),

    onSubmit() {
      this.updateDeviceAction(this.bodyRequest);
    },
  },

  data: () => ({
    dialog: false,
    currencies: ["USD", "NOK"],
    currencyValues: [0, 1],
    durations: [1, 2, 3, 4, 5, 6, 7, 8],
    durationValue: 1,
  }),
};
</script>
