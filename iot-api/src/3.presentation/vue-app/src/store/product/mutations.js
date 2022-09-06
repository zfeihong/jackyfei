import * as types from "./types";

const mutations = {
  [types.GET_PRODUCT_LISTS](state, lists) {
    state.lists = lists;
  },

  [types.LOADING_PRODUCT](state, value) {
    state.loading = value;
  },
};

export default mutations;
