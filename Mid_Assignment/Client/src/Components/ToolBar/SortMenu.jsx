import { MenuButton, Button, Menu, MenuList, MenuOptionGroup, MenuItemOption, MenuDivider } from "@chakra-ui/react";

export function SortMenu(props) {
  const {
    sortOrders = [],
    sortOptions = [],
    defaultOrder,
    defaultOption,
    closeOnSelect = true,
    buttonText,
    buttonProps = {},
    ...otherProps } = props;

  return (
    <Menu closeOnSelect={closeOnSelect}>
      <MenuButton as={Button} {...buttonProps}>
        {buttonText}
      </MenuButton>
      <MenuList>
        <MenuOptionGroup
          defaultValue={defaultOrder}
          title='Order'
          type='radio'
        >
          {sortOrders.map(order =>
            <MenuItemOption
              key={order.value}
              value={order.value}
            >
              {order.text}
            </MenuItemOption>
          )}
        </MenuOptionGroup>
        <MenuDivider />
        <MenuOptionGroup
          defaultValue={defaultOption}
          title='Option'
          type='radio'
        >
          <MenuItemOption value=''>(none)</MenuItemOption>
          {sortOptions.map(option =>
            <MenuItemOption
              key={option.value}
              value={option.value}
            >
              {option.text}
            </MenuItemOption>
          )}
        </MenuOptionGroup>
      </MenuList>
    </Menu>
  )
}