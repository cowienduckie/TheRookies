import { Container } from "@chakra-ui/react";
import { Outlet } from "react-router-dom";
import { AuthContext } from "./Contexts/AuthContext";
import { useState } from "react";
import { Header } from "./Components";
import { TOKEN_KEY } from "./Constants/SystemConstants";

function App() {
  const [authenticated, setAuthenticated] = useState(localStorage.getItem(TOKEN_KEY) != null);

  return (
    <AuthContext.Provider value={{ authenticated, setAuthenticated }}>
      <Header />
      <Container maxW="container.xl" p={0}>
        <Outlet />
      </Container>
    </AuthContext.Provider>
  )
}

export default App
