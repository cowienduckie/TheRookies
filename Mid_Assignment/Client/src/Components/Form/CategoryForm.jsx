import { Center, Heading, VStack, Button, ButtonGroup } from "@chakra-ui/react";
import { useState } from "react";
import { Form, useNavigate } from "react-router-dom";
import {
  minLengthValidate,
  requiredValidate,
} from "../../Helpers/InputValidations";
import { TextInput } from "./TextInput";

export function CategoryForm(props) {
  const { path, method, title, data } = props;

  const [category, setCategory] = useState(data);

  const navigate = useNavigate();

  return (
    <Form action={path} method={method}>
      <Center>
        <VStack w="60%" p={10} spacing={5}>
          <Heading>{title}</Heading>
          <TextInput
            label="Category Name"
            name="name"
            type="text"
            isRequired={true}
            defaultValue={category.name}
            handleChange={(value) => {
              setCategory({ ...category, ["name"]: value });
            }}
            validateInput={(input) => {
              let errorMessage =
                requiredValidate(input) || minLengthValidate(input, 2);

              let isError = errorMessage !== "";

              return { isError, errorMessage };
            }}
          />
          <ButtonGroup>
            <Button
              mt={4}
              p={5}
              colorScheme="red"
              onClick={() => {
                navigate(-1);
              }}
            >
              Cancel
            </Button>
            <Button mt={4} p={5} type="submit" colorScheme="teal">
              Submit
            </Button>
          </ButtonGroup>
        </VStack>
      </Center>
    </Form>
  );
}
