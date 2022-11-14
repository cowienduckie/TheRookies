import { Box, Divider, Heading } from "@chakra-ui/react";
import { Outlet } from "react-router-dom";

export function loader() {}

export function BookManagePage() {
  return (
    <Box p={10}>
      <Heading size="xl">BOOK PAGE</Heading>
      <Divider my={7} />
      <Outlet />
    </Box>
  );
}
