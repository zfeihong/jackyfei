import api from "@/api/api-v1-config";

export async function getProductAxios() {
  return await api.get("Product");
}

export async function deleteProductAxios(id) {
  return await api.delete("Product/" + id);
}

export async function postProductAxios(product) {
  return await api.post("Product", product);
}

export async function deleteDeviceAxios(id) {
  return await api.delete("Device/" + id);
}

export async function postDeviceAxios(device) {
  return await api.post("Device", device);
}

export async function putDeviceAxios(device) {
  return await api.put(`Device/${device.id}`, device);
}
