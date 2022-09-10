import * as types from "./types";
import {
  getProductAxios,
  deleteProductAxios,
  postProductAxios,
  deleteDeviceAxios,
  postDeviceAxios,
  putDeviceAxios,
} from "@/store/product/services";

//获取产品列表
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

//移除产品 异步
export async function removeProductAction({ commit }, payload) {
  commit(types.LOADING_PRODUCT, true);
  try {
    await deleteProductAxios(payload);
    commit(types.REMOVE_PRODUCT, payload);
  } catch (e) {
    alert(e);
    console.log(e);
  }

  commit(types.LOADING_PRODUCT, false);
}

//添加产品 异步 action
export async function addProductAction({ commit }, payload) {
  commit(types.LOADING_PRODUCT, true);

  try {
    const { data } = await postProductAxios(payload);
    payload.id = data;
    payload.productCategory = [];
    commit(types.ADD_PRODUCT, payload);
  } catch (e) {
    alert(e);
    console.log(e);
  }

  commit(types.LOADING_PRODUCT, false);
}

// 非异步 action
export function getDevicesOfSelectedProductIdAction({ commit }, payload) {
  commit(types.GET_DEVICE_OF_SELECTED_PRODUCTID, payload);
}

//移除设备 异步 action
export async function removeDeviceAction({ commit }, payload) {
  commit(types.LOADING_PRODUCT, true);

  try {
    await deleteDeviceAxios(payload);
    commit(types.REMOVE_DEVICE, payload);
  } catch (e) {
    alert(e);
    console.log(e);
  }

  commit(types.LOADING_PRODUCT, false);
}

//添加设备 异步 action
export async function addDeviceAction({ commit }, payload) {
  commit(types.LOADING_PRODUCT, true);

  try {
    const { data } = await postDeviceAxios(payload);
    payload.id = data;
    commit(types.ADD_DEVICE, payload);
  } catch (e) {
    alert(e);
    console.log(e);
  }

  commit(types.LOADING_PRODUCT, false);
}

//更新设备
export async function updateDeviceAction({ commit }, payload) {
  commit(types.LOADING_PRODUCT, true);

  try {
    await putDeviceAxios(payload);
    commit(types.UPDATE_DEVICE, payload);
  } catch (e) {
    alert(e);
    console.log(e);
  }

  commit(types.LOADING_PRODUCT, false);
}
