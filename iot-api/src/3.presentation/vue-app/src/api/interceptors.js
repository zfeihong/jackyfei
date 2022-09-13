import { getToken } from "@/auth/auth.service";

export function interceptorsInit(axiosInstance) {
  axiosInstance.interceptors.request.use(function (options) {
    const jwtToken = getToken();

    if (jwtToken) {
      options.headers["Authorization"] = `Bearer ${jwtToken}`;
    }

    return options;
  });

  axiosInstance.interceptors.response.use(
    (response) => response,
    (error) =>
      Promise.reject((error.response && error.response.data) || "一些错误信息")
  );

  return axiosInstance;
}
