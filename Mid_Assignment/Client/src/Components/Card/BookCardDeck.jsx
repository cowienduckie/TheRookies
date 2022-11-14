import { Wrap } from "@chakra-ui/react";
import { BookCard } from "./BookCard";

export function BookCardDeck(props) {
  const { data, path, ...otherProps } = props;

  return (
    <Wrap spacing={10} p={0} w={"full"} align={"flex-start"}>
      {data.map((book) => (
        <BookCard key={book.id} data={book} path={path} />
      ))}
    </Wrap>
  );
}
