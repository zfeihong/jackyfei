const getters = {
  lists: (state) => state.lists,
  loading: (state) => state.loading,
  Jackyfei: function (state) {
    const re = state.lists;
    return re;
  },
};
export default getters;
