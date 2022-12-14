import {
  ArrowLeftIcon,
  ArrowRightIcon,
  ChevronLeftIcon,
  ChevronRightIcon,
} from "@chakra-ui/icons";
import {
  Button,
  ButtonGroup,
  Center,
  Divider,
  IconButton,
  Spacer,
  Text,
} from "@chakra-ui/react";

export function Pagination(props) {
  const {
    pageIndex,
    pageSize,
    totalPage,
    totalRecord,
    hasNextPage,
    hasPreviousPage,
    colorScheme,
    handleChange,
    queryState,
    ...otherProps
  } = props;

  const handleOnClick = (value) => {
    handleChange({ ...queryState, ["pageIndex"]: value });
  };

  return (
    <Center {...otherProps}>
      <Spacer />
      <ButtonGroup spacing={5}>
        <IconButton
          aria-label="First"
          icon={<ArrowLeftIcon />}
          onClick={() => {
            handleOnClick(1);
          }}
          variant="outline"
          colorScheme={colorScheme}
          disabled={!hasPreviousPage}
        />
        <IconButton
          aria-label="Prev"
          icon={<ChevronLeftIcon />}
          onClick={() => {
            handleOnClick(pageIndex - 1);
          }}
          variant="outline"
          colorScheme={colorScheme}
          disabled={!hasPreviousPage}
        />
        <Button
          aria-label="Curr"
          value={pageIndex}
          variant="outline"
          colorScheme={colorScheme}
        >
          {pageIndex}
        </Button>
        <IconButton
          aria-label="Next"
          icon={<ChevronRightIcon />}
          onClick={() => {
            handleOnClick(pageIndex + 1);
          }}
          variant="outline"
          colorScheme={colorScheme}
          disabled={!hasNextPage}
        />
        <IconButton
          aria-label="Last"
          icon={<ArrowRightIcon />}
          onClick={() => {
            handleOnClick(totalPage);
          }}
          variant="outline"
          colorScheme={colorScheme}
          disabled={!hasNextPage}
        />
      </ButtonGroup>
      <Spacer />
      <Text>Total page: {totalPage}</Text>
      <Text mx={5}>|</Text>
      <Text>Total record: {totalRecord}</Text>
    </Center>
  );
}
