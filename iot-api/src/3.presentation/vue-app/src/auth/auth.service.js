import api from "@/api/api-v1-config";

import * as jwt from "jsonwebtoken";
const key = "token";

export async function loginUserAxios(login) {
  return await api.post("Users/auth", login);
}

export function getToken() {
  return localStorage.getItem(key);
}

export function logOut() {
  localStorage.clear();
  window.location = "/login";
}

//本地token是否过期
export function isTokenFromLocalStorageValid() {
  const token = localStorage.getItem(key);
  if (!token) {
    return false;
  }

  const decoded = jwt.decode(token);
  const expiresAt = decoded.exp * 1000;
  const dateNow = Date.now();

  return dateNow <= expiresAt;
}

// 从token中获取email
export function getUserEmailFromToken() {
  const token = localStorage.getItem(key);

  if (!token) return false;

  const decoded = jwt.decode(token);
  // alert(decoded.email);

  return decoded.email;
}
