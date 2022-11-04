import { FormControl, FormLabel, Input, FormHelperText, FormErrorMessage } from "@chakra-ui/react";
import { useState } from "react";

export function TextInput(props) {
  const { label, name, type, defaultValue, isRequired, handleChange, validateInput, helperText, ...otherProps } = props;

  const [input, setInput] = useState(defaultValue);

  const handleInputChange = (event) => {
    handleChange(event.target.value);
    setInput(event.target.value);
  };

  const { isError, errorMessage } = validateInput(input);

  return (
    <FormControl isInvalid={isError} isRequired={isRequired}>
      <FormLabel>{label}</FormLabel>
      <Input type={type} value={input} name={name} onChange={handleInputChange} />
      {!isError ? (
        <FormHelperText>
          {helperText}
        </FormHelperText>
      ) : (
        <FormErrorMessage>{errorMessage}</FormErrorMessage>
      )}
    </FormControl>
  )
}