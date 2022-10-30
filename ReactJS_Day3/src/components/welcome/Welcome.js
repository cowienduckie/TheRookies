import './Welcome.css'

export function Welcome(props) {
  const welcomeStyle = {
    color: props.color,
    backgroundColor: props.backgroundColor
  };

  return (
    <div className='person-info' style={welcomeStyle}>
      <p><strong>Name: {props.name}</strong></p>
      <p>Age: {props.age}</p>
    </div>
  );
}