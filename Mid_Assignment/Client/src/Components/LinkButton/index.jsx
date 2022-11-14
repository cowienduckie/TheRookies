import { Button, Icon, LinkBox } from "@chakra-ui/react";
import { NavLink } from "react-router-dom";

export function LinkButton(props) {
  const { path, label, colorScheme, variant, text, icon, ...otherProps } =
    props;

  return (
    <LinkBox as={NavLink} to={path}>
      <Button
        aria-label={label}
        colorScheme={colorScheme}
        variant={variant}
        {...otherProps}
      >
        {text}
        {icon && <Icon as={icon} />}
      </Button>
    </LinkBox>
  );
}
