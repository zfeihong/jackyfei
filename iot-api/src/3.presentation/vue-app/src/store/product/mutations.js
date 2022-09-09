import * as types from "./types";

const mutations = {
  [types.GET_PRODUCT_LISTS](state, lists) {
    state.lists = lists;
  },

  [types.LOADING_PRODUCT](state, value) {
    state.loading = value;
  },

  [types.REMOVE_PRODUCT](state, id) {
    state.lists = state.lists.filter((tl) => tl.id !== id);
    state.deviceOfSelectedProductId = [];
  },

  [types.ADD_PRODUCT](state, product) {
    state.lists.unshift(product);
  },

  [types.GET_DEVICE_OF_SELECTED_PRODUCTID](state, device) {
    state.deviceOfSelectedProductId = device;
  },

  [types.REMOVE_DEVICE](state, id) {
    state.deviceOfSelectedProductId = state.deviceOfSelectedProductId.filter(
      (tp) => tp.id !== id
    );
  },

  [types.ADD_DEVICE](state, device) {
    state.deviceOfSelectedProductId.unshift(device);
  },

  [types.UPDATE_DEVICE](state, payload) {
    const packageIndex = state.deviceOfSelectedProductId.findIndex(
      (pl) => pl.id === payload.id
    );
    state.deviceOfSelectedProductId[packageIndex] = payload;

    const listIndex = state.lists.findIndex(
      (l) => l.id === state.deviceOfSelectedProductId.listId
    );
    state.lists[listIndex] = state.deviceOfSelectedProductId;
  },
};

export default mutations;
