
using FluentAssertions;
using WPF_StudRegSys; 
namespace WPF_StudRegSys.test
 {
    public class UnitTest1
    {
        [Fact]
        public void When_Created_GPA_Should_Be_Zero()
        {
            Student student = new Student();
            student.GPA.Should().Be(0);


        }



    }
       
        
 }
