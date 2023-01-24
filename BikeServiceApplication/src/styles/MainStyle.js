import {StyleSheet} from 'react-native';

const mainStyle = StyleSheet.create({
  circleBtn: {
    alignSelf: 'flex-end',
    position: 'absolute',
    justifyContent: 'center',
    backgroundColor: '#249ef0',
    bottom: 10,
    right: 10,
    width: 60,
    height: 60,
    padding: 5,
    borderRadius: 60 / 2
  },
  button: {
    alignItems: 'center',
    marginTop: 10,
    marginLeft:'auto',
    marginRight:'auto',
    justifyContent: 'center',
    width: 100,
    padding: 5,
    backgroundColor: '#249ef0',
    borderRadius: 5,
    zIndex: 100
  },
  buttonText : {
    color: 'white',
    textAlign: 'center'
  },
  conteiner:{
    flexDirection: 'column',
    flex: 1
  },
  buttonDisabled: {
    alignItems: 'center',
    marginLeft:'auto',
    marginRight:'auto',
    marginTop: 10,
    justifyContent: 'center',
    width: 100,
    padding: 5,
    borderRadius: 5,
    zIndex: 100,
    backgroundColor: 'grey'
  },
  buttonEnabled: {
    alignItems: 'center',
    marginLeft:'auto',
    marginRight:'auto',
    marginTop: 10,
    justifyContent: 'center',
    width: 100,
    padding: 5,
    borderRadius: 5,
    zIndex: 100,
    backgroundColor: '#249ef0',
  },
  taskHeader:{
    color: '#000000',
    borderWidth: 1,
    marginLeft: 'auto',
    marginRight: 'auto',
    marginTop: 10,
    width: 300,
    padding: 5,
    backgroundColor: 'white',
    height: 30
  },
  taskDescription:{
    color: '#000000',
    borderWidth: 1,
    marginLeft: 'auto',
    marginRight: 'auto',
    marginTop: 10,
    width: 300,
    padding: 5,
    backgroundColor: 'white',
    height: 'auto',
    textAlignVertical: "top"
  },
  text : {
    color: '#000000',
    textAlign: 'center',
    marginTop: 10,
    fontSize: 20,
    marginTop: 10
  },
});

export default mainStyle;