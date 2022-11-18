import React, {Component} from 'react';
import {View, StyleSheet, Text, TextInput, TouchableOpacity, BackHandler} from 'react-native';

import TasksController from '../controllers/TasksController';
import Task from '../objects/Task'

class EditTaskScreen extends Component {
  constructor(props){
    super(props);

    this.handleBackButton.bind(this)


    this.state = {
      id: Task.task.Id,
      header: Task.task.Header,
      description: Task.task.Description,
      state: Task.task.State
    };

  }

  componentDidMount(){
    this.focusListener = this.props.navigation.addListener('focus', () => {
      BackHandler.addEventListener('hardwareBackPress', this.handleBackButton);
    });
  }
  
  handleBackButton(){
    BackHandler.removeEventListener('hardwareBackPress', this.handleBackButton);
    this.props.navigation.navigate('ControllPanel');
    return true;
  } 

  componentWillUnmount() {
    BackHandler.removeEventListener('hardwareBackPress', this.handleBackButton);
  }

  update(){
    Task.task.Header = this.state.header;
    Task.task.Description = this.state.description;

    var ok = TasksController.updateTask();

    if(ok === true){
      this.handleBackButton();
    }
  }

  headerInputTextChange = (newText) => {
    this.setState({ header: newText })
  }

  descriptionInputTextChange = (newText) => {
    this.setState({ description: newText })
  }

  render() {
    return (
      <View >
        <TextInput 
          value={this.state.header}
          style={styles.header}
          placeholder="tytuł" 
          placeholderTextColor="gray" 
          onChangeText={this.headerInputTextChange}
        />
        <TextInput 
          value={this.state.description}
          multiline={true}
          style={styles.description}
          placeholder="opis" 
          placeholderTextColor="gray" 
          onChangeText={this.descriptionInputTextChange}
        />
         <TouchableOpacity style={styles.searchButton} onPress={this.update.bind(this)}>
            <Text style={styles.buttonText}>Aktualizuj</Text>
        </TouchableOpacity>
      </View>
    );
  }
}

export default EditTaskScreen;

const styles = StyleSheet.create({
  header:{
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
  description:{
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
  searchButton: {
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
  buttonText:{
    color: 'white'
  }
});
