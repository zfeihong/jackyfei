import api from "@/api/api-v1-config";

export async function getProductAxios() {
  return await api.get("Product");
}
