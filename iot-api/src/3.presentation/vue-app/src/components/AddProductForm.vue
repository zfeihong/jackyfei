<template>
  <v-row justify="center">
    <v-dialog v-model="dialog" persistent max-width="600px">
      <template v-slot:activator="{ on, attrs }">
        <v-btn
          style="margin-top: 1rem"
          rounded
          color="light-blue"
          dark
          v-bind="attrs"
          v-on="on"
        >
          <v-icon left>mdi-plus</v-icon>
          添加新产品
        </v-btn>
      </template>
      <v-card>
        <form
          @submit.prevent="
            addProductAction(bodyRequest);
            bodyRequest = {};
          "
        >
          <v-card-title>
            <span class="headline">添加新产品</span>
          </v-card-title>
          <v-card-text>
            <v-container>
              <v-row>
                <v-col cols="12" sm="6">
                  <v-text-field
                    label="id"
                    v-model="bodyRequest.id"
                    required
                  ></v-text-field>
                </v-col>
                <v-col cols="12" sm="6">
                  <v-autocomplete
                    :items="productCategory"
                    label="分类"
                    v-model="bodyRequest.category"
                    required
                  ></v-autocomplete>
                </v-col>
                <v-col cols="12" sm="6">
                  <v-text-field
                    label="代码"
                    v-model="bodyRequest.code"
                    required
                  ></v-text-field>
                </v-col>
                <v-col cols="12" sm="6">
                  <v-text-field
                    label="名称"
                    v-model="bodyRequest.name"
                    required
                  ></v-text-field>
                </v-col>
                <v-col cols="12">
                  <v-textarea
                    label="备注"
                    v-model="bodyRequest.remark"
                    required
                  ></v-textarea>
                </v-col>
              </v-row>
            </v-container>
            <small>*为必填字段</small>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" text @click="dialog = false">
              关闭
            </v-btn>
            <v-btn
              color="blue darken-1"
              text
              @click="dialog = false"
              type="submit"
            >
              保存
            </v-btn>
          </v-card-actions>
        </form>
      </v-card>
    </v-dialog>
  </v-row>
</template>

<script>
import { getProductCategory } from "@/helpers/collections";
import { mapActions } from "vuex";

export default {
  name: "AddProductForm2",

  data: () => ({
    bodyRequest: {
      id: "",
      productCategory: "",
      code: "",
    },

    dialog: false,
    productCategory: getProductCategory(),
  }),

  methods: {
    ...mapActions("productModule", ["addProductAction"]),
  },
};
</script>
