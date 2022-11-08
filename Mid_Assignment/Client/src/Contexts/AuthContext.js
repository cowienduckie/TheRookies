import { createContext } from "react";
import { TOKEN_KEY } from "../Constants/SystemConstants";

export const AuthContext = createContext({
  authenticated: localStorage.getItem(TOKEN_KEY) != null,
  setAuthenticated: (auth) => {}
});