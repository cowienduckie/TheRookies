import { Icon, IconButton } from "@chakra-ui/react";
import { Form } from "react-router-dom";

export function FormIconButton(props) {
  const { path, method, onSubmit, label, colorScheme, icon, ...otherProps } = props;

  return (
    <Form
      action={path}
      method={method}
      onSubmit={(event) => onSubmit(event)}
    >
      <IconButton
        type='submit'
        aria-label={label}
        colorScheme={colorScheme}
        icon={<Icon as={icon} />}
      />
    </Form>
  );
}