import { Box, Divider, Heading } from "@chakra-ui/react";
import { Outlet } from "react-router-dom";

export function loader() {}

export function BorrowRequestManagePage() {
  return (
    <Box p={10}>
      <Heading size="xl">BORROW REQUEST PAGE</Heading>
      <Divider my={7} />
      <Outlet />
    </Box>
  );
}
