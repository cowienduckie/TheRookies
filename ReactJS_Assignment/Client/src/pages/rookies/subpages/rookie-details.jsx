import { HStack, VStack, Heading, Text, Image } from "@chakra-ui/react";
import { useLoaderData } from "react-router-dom";
import { getRookieById } from "../../../apis/rookies";

export async function loader({ params }) {
  const rookie = await getRookieById(params.rookieId);

  if (!rookie) {
    throw new Response("", {
      status: 404,
      statusText: 'Not Found'
    })
  }

  return rookie;
}

export function RookieDetailsPage() {
  const rookieInfo = useLoaderData();

  return <>
    <HStack w='full' spacing={10} p={0} align='top'>
      <Image
        borderRadius='full'
        boxSize='30%'
        src='https://dummyimage.com/300x300/'
        alt='Dummy image'
        m='auto' />
      <VStack w='70%' p={10} spacing={10} bgColor='gray.50' align='left'>
        <Heading size='2xl'>{rookieInfo.fullName.toUpperCase()}</Heading>
        <Text fontSize='2xl'><strong>ID:</strong> {rookieInfo.id}</Text>
        <Text fontSize='2xl'><strong>Gender:</strong> {rookieInfo.gender}</Text>
        <Text fontSize='2xl'><strong>Date of Birth:</strong> {rookieInfo.dateOfBirth}</Text>
        <Text fontSize='2xl'><strong>Birth Place:</strong> {rookieInfo.birthPlace}</Text>        
      </VStack>
    </HStack>
  </>
}