import { FormControl, FormLabel, Select, FormHelperText, FormErrorMessage } from "@chakra-ui/react";
import { useState } from "react";

export function SelectInput(props) {
  const { label, name, defaultValue, options, isRequired, handleChange, validateInput, helperText, placeholder, ...otherProps } = props;

  const [input, setInput] = useState(defaultValue);

  const handleInputChange = (event) => {
    handleChange(event.target.value);
    setInput(event.target.value);
  };

  const { isError, errorMessage } = validateInput(input);

  return (
    <FormControl
      isInvalid={isError}
      isRequired={isRequired}
    >
      <FormLabel>{label}</FormLabel>
      <Select
        name={name}
        value={input}
        placeholder={placeholder}
        onChange={handleInputChange}
      >
        {options.map(option =>
          <option key={option.value} value={option.value}>{option.text}</option>
        )}
      </Select>
      {!isError ? (
        <FormHelperText>{helperText}</FormHelperText>
      ) : (
        <FormErrorMessage>{errorMessage}</FormErrorMessage>
      )}
    </FormControl>
  )
}