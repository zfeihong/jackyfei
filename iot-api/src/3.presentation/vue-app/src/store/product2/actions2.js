import * as types from "./types";
import { getProductAxios as getProductAxios } from "@/store/product/services";

//
export async function getProductAction({ commit }) {
  commit(types.LOADING_PRODUCT, true);

  try {
    const { data } = await getProductAxios();
    commit(types.GET_PRODUCT_LISTS, data.lists);
  } catch (e) {
    alert(e);
    console.log(e);
  }

  commit(types.LOADING_PRODUCT, false);
}
