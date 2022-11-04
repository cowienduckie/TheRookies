import { Center, Heading, VStack, Button, ButtonGroup } from "@chakra-ui/react";
import { useState } from "react";
import { Form, useNavigate } from "react-router-dom";
import { minLengthValidate, requiredValidate } from "../../helpers/inputValidation";
import { SelectInput } from "../form-input/SelectInput";
import { TextInput } from "../form-input/TextInput";

export function RookieForm(props) {
  const { path, method, title, data, ...otherProps } = props;

  const [rookie, setRookie] = useState(data);

  const navigate = useNavigate()

  const genders = [
    { text: 'Male', value: 'Male' },
    { text: 'Female', value: 'Female' },
    { text: 'Other', value: 'Other' },
  ];

  return (
    <Form
      action={path}
      method={method}>
      <Center>
        <VStack w='60%' p={10} spacing={5}>
          <Heading>{title}</Heading>
          <TextInput
            label='First Name'
            name='firstName'
            type='text'
            isRequired={true}
            defaultValue={rookie.firstName}
            handleChange={(value) => {
              setRookie({ ...rookie, ['firstName']: value })
            }}
            validateInput={(input) => {
              let errorMessage = (
                requiredValidate(input) ||
                minLengthValidate(input, 2)
              );

              let isError = errorMessage !== '';

              return { isError, errorMessage };
            }} />
          <TextInput
            label='Last Name'
            name='lastName'
            type='text'
            isRequired={true}            
            defaultValue={rookie.lastName}
            handleChange={(value) => {
              setRookie({ ...rookie, ['lastName']: value })
            }}
            validateInput={(input) => {
              let errorMessage = (
                requiredValidate(input) ||
                minLengthValidate(input, 2)
              );

              let isError = errorMessage !== '';

              return { isError, errorMessage };
            }} />
          <SelectInput
            label='Gender'            
            name='gender'
            isRequired={true}
            placeholder='Select gender'
            options={genders}            
            defaultValue={rookie.gender}
            handleChange={(value) => {
              setRookie({ ...rookie, ['lastName']: value })
            }}
            validateInput={(input) => {
              let errorMessage = (
                requiredValidate(input)
              );

              let isError = errorMessage !== '';

              return { isError, errorMessage };
            }} />
          <TextInput
            label='Date of Birth'            
            name='dateOfBirth'
            type='date'            
            defaultValue={rookie.dateOfBirth}
            isRequired={true}
            handleChange={(value) => {
              setRookie({ ...rookie, ['dateOfBirth']: value })
            }}
            validateInput={(input) => {
              let errorMessage = (
                requiredValidate(input)
              );

              let isError = errorMessage !== '';

              return { isError, errorMessage };
            }} />
          <TextInput
            label='Birth Place'
            name='birthPlace'
            type='text'
            isRequired={true}            
            defaultValue={rookie.birthPlace}
            handleChange={(value) => {
              setRookie({ ...rookie, ['birthPlace']: value })
            }}
            validateInput={(input) => {
              let errorMessage = (
                requiredValidate(input) ||
                minLengthValidate(input, 2)
              );

              let isError = errorMessage !== '';

              return { isError, errorMessage };
            }} />
          <ButtonGroup>
            <Button
              mt={4}
              p={5}
              colorScheme='red'
              onClick={() => {
                navigate(-1);
              }}>
              Cancel
            </Button>
            <Button
              mt={4}
              p={5}
              type='submit'
              colorScheme='teal'>
              Submit
            </Button>
          </ButtonGroup>
        </VStack>
      </Center>
    </Form>
  )
}