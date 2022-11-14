import { Container } from "@chakra-ui/react";
import { Outlet } from "react-router-dom";
import { Header } from "./Components";
import { BorrowRequestState } from "./Contexts/BorrowRequestState";
import { AuthState } from "./Contexts/AuthState";

function App() {
  return (
    <AuthState>
      <BorrowRequestState>
        <Header />
        <Container maxW="container.xl" p={0}>
          <Outlet />
        </Container>
      </BorrowRequestState>
    </AuthState>
  );
}

export default App;
