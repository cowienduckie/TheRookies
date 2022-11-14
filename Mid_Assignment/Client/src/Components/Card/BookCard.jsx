import {
  Heading,
  Image,
  LinkBox,
  Tag,
  Text,
  VStack,
  Wrap,
} from "@chakra-ui/react";
import { NavLink } from "react-router-dom";

export function BookCard(props) {
  const { data, path, ...otherProps } = props;

  return (
    <LinkBox
      as={NavLink}
      to={path + `/${data.id}`}
      w={"20%"}
      p={5}
      borderWidth={1}
      rounded={"md"}
    >
      <Image
        src={data.cover}
        alt="Book cover"
        w={200}
        h={300}
        m={"auto"}
        objectFit={"fill"}
      />
      <Heading size={"md"} my={5} noOfLines={2}>
        {data.name.toUpperCase()}
      </Heading>
      {data.description && data.description !== "" && (
        <Text mb={3} noOfLines={2}>
          {data.description}
        </Text>
      )}
      <Wrap>
        {data.categories.map((c) => (
          <Tag key={c.id} size={"sm"} colorScheme={"messenger"}>
            {c.name}
          </Tag>
        ))}
      </Wrap>
    </LinkBox>
  );
}
