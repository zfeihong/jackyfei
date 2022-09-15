import {
  required,
  email,
  minLength,
  maxLength,
} from "vuelidate/lib/validators";

export default {
  login: {
    email: { required, email },
    password: { required, minLength: minLength(8) },
  },
  code: {
    required,
    maxLength: maxLength(90),
  },
};
