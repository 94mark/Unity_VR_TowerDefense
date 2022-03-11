# 타워 디펜스 VR게임 제작
![image](https://user-images.githubusercontent.com/90877724/157863769-6c90cab6-9c1f-4ab0-b2b9-9098f1472153.png)
## 1. 프로젝트 개요
### 1.1 개발 인원/기간 및 포지션
- 1인, 총 5일 소요
- 프로그래밍 (VR환경 구축 및 개발)
### 1.2 개발 환경
- unity 2020.3.16f
- VR HMD : Oculus Quest 2
- 언어 : C#
- OS : Window 10
## 2. 개발 단계
- 에픽 게임즈 VR FPS < Bullet Train > 모티프 
- 프로토타입 버전: 기본 기능 구현 및 PC 버전
- 알파 타입 버전 : VR 컨트롤러 입력 대응 
## 3. 핵심 구현 내용 
### 3.1 플레이어 조작
- 이동 및 점프
	- 전후좌우 이동은 CharacterController의 Move 함수 사용
	- 등가속도 운동 공식이 적용된 yVelocity 값에 중력을 더하고 수직 속도에 점프 크기를 넣어 Jump 기능 구현
	- TransformDirection() 함수를 사용하여 카메라가 바라보는 방향으로 사용자의 입력 방향을 바꿈(사용자가 바라보는 방향으로 이동 구현)
- 텔레포트 기능 
	- Ray가 맞은 지점이 Terrain일 경우 텔레포트 UI와 Line Renderer 활성화, SetPosition() 함수를 사용해 시작점과 끝점에 라인 그리기
	- 텔레포트 UI의 크기가 거리에 따라 보정되도록 original scale 설정, normal 벡터 방향을 forward로 해 UI가 하늘을 바라보도록 그림
	- Get/GetDown/GetUp 메서드를 사용하여 컨트롤러의 버튼 입력 시의 처리와 순서 재정리
	- PostProcessing에서 사용하는 프로파일에서 Motion Blur 효과를 얻어오고 Warp() 함수 구현에 적용, 시작점에서 도착점까지의 이동을 선형 보간(Lerp)으로 처리
- Grab하기
	- Physics.OverlapSphere() 함수를 사용하여 컨트롤러의 위치를 중심으로 grabRange 안에 들어온 grabbedLayer 물체 검출
	- grabbedObject를 계속 잡고 있을 수 있도록 컨트롤러를 parent로 등록, is kinematic 옵션을 활성화하여 grab 되어있는 동안 물리 기능 비활성화
	- 각속도를 적용하여 물체가 회전 축을 바탕으로 회전하며 날아가는 기능 구현, deltaRotation의 ToAngleAxis 함수를 사용해 angle, axis, 각속도 구함
	- SphereCast() 함수를 사용해 캡슐 형태의 영역 내에 물체 충돌 여부를 검출하고 충돌 시 물체를 끌어당기는 GrabbingAnimation() 코루틴 함수를 실행하여 원거리 물체를 끌어당기는 기능 구현
### 3.2 무기
- Gun 
	- 조준점 Crosshair 활성화, IndexTrigger 버튼 입력 시 Ray를 발사
	- 총알 파편 파티클 시스템 & 총알 발사 사운드 실행
	- IndexTrigger 당길 시, ARAVRInput.PlayVibration 함수를 사용해 진동 구현 
- Bomb
	- 폭발 시 explosion 이펙트 및 sound 활성화
	- Physics.OverlapSphere() 함수를 사용하여 특정 반경 내 검출하고자 하는 레이어(Drone)를 가져오고, foreach문을 이용해 검출된 모든 객체들의 Collider배열을 Destroy
### 3.3 적 드론 제작
- 드론 FSM 상태 지정(대기, 이동, 공격, 피격, 죽음)
- Navigation AI를 적용해 Tower까지 최단거리 Path Finding
- 피격 시 피격 효과(이동 불가/머터리얼 색 변경) Damge() 코루틴 함수 실행
- 드론 생성 시 random으로 뽑힌 한 spawnPoints에서 Instantiate() 
## 4. 문제 해결 내용
### 4.1 플레이어가 타워에서 바닥으로 떨어질 때 순간 이동하는 듯이 부자연스럽게 떨어지는 현상 발생
- 수직 속도인 yVelocity가 타워 위에 있을 때 떨어지지 않는데도 계속 누적돼 실제 공중에 있을 때 그 값이 너무 커져서 발생한 문제
- 실제로 바닥에 있을 때는 반발력에 해당하는 수직 항력이 발생
- 바닥에 있는 경우 yVelocity를 0으로 초기화시켜 문제 해결
```
if(cc.isGrounded)
{
	yVelocity = 0;
}
```
