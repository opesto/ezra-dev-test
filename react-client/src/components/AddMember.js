import React, { Component } from 'react'
import './AddEditMember.css'

export class AddMember extends Component {
  static displayName = AddMember.name

  constructor(props) {
    super(props);
    this.state = {name:'', email:''};
    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleSubmit(event) {
    event.preventDefault();
    this.addMember(this.state.name, this.state.email);
  }

  handleChange = (event) => {
    let nam = event.target.name;
    let val = event.target.value;
    this.setState({[nam]: val});
  }

  async addMember(name, email) {
    try {
      await fetch('http://localhost:5000/members/addMember/'+name+'/'+email, {
        method: 'Post',
      });
      alert('A member was submitted successfully: ' + name + ', '+ email);
      this.props.history.push("/");
    } catch (err) {
      console.log(err);
    }
  }

  render() {
      return (
        <div className="card">
          <form onSubmit={this.handleSubmit}>
              <label>
                Name:
                <input className = "textBox" type="text" name='name' onChange={this.handleChange} />
              </label>
              <br></br>
              <label>
                Email:
                <input className = "textBox" type="text" name='email' onChange={this.handleChange} />
              </label>
              <br></br>
              <input className="btn btn-primary" type="submit" value="Add" />
            </form>
        </div>
      )
  }
}
