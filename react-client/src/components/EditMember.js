import React, { Component } from 'react'
import './AddEditMember.css'

export class EditMember extends Component {
  static displayName = EditMember.name

  constructor(props) {
    super(props)
    let member = this.props.location.state.member;
    this.state = {
      id: member.id,
      name: member.name,
      email: member.email
    };
    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleSubmit(event) {
    event.preventDefault();
    this.editMember(this.state.id, this.state.name, this.state.email);
  }

  handleChange = (event) => {
    let nam = event.target.name;
    let val = event.target.value;
    this.setState({[nam]: val});
  }

  async editMember(id, name, email) {
    try {
      await fetch('http://localhost:5000/members/update/'+id+'/'+name+'/'+email, {
        method: 'Patch',
      });
      alert('A member was updated successfully: ' + name + ', '+ email);
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
              <input className = "textBox" type="text" name='name' value={this.state.name} onChange={this.handleChange} />
            </label>
            <br></br>
            <label>
              Email: 
              <input className = "textBox" type="text" name='email' value={this.state.email} onChange={this.handleChange} />
            </label>
            <br></br>
            <input className="btn btn-primary" type="submit" value="Edit" />
          </form>
        </div>
    )
}

}